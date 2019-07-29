using Flunt.Validations;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Entities;
using SIGLO.Shared.Enums;
using SIGLO.Shared.Messages;
using System.Text;

namespace SIGLO.Domain.Entities
{
    public class User : Entity, IValidatable
    {
        private string _confirmPassword;

        public User() { }

        public User(NameOrDescription name,
                    string login,
                    string password,
                    string confirmPassword,
                    long nivel,
                    decimal percentM,
                    decimal percentB,
                    long createdByUser,
                    CompanyAndBranchOffice companyAndBranchOfficeId,
                    string machineNameOrIP)
        {
            Name = name;
            Login = login;
            Password = Encrypt(password);
            _confirmPassword = Encrypt(confirmPassword);
            Active = EActiveDeactive.Active;
            Nivel = nivel;
            PercentM = percentM;
            PercentB = percentB;

            CreatedDateBy created = new CreatedDateBy(createdByUser);
            UpdatedDateBy updated = new UpdatedDateBy(null);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(companyAndBranchOfficeId.CompanyId, companyAndBranchOfficeId.BranchOfficeId);
            Audit audit = new Audit(created, updated, companyAndBranchOffice, ERecordStatus.Active, machineNameOrIP);
            Audit = audit; 

            Validate();
        }
        public void Update(NameOrDescription name,
            string login,
            string password,
            string confirmPassword,
            long nivel,
            long active,
            decimal percentM, 
            decimal percentB, 
            long? updatedByUser)
        {
            Name = name;
            Login = login;
            Password = password;
            _confirmPassword = confirmPassword;
            Active = (EActiveDeactive) active;
            Nivel = nivel;
            PercentM = percentM;
            PercentB = percentB;

            Audit.Update(updatedByUser);
            Validate();
        }

        public void Activate()
        {
            Active = EActiveDeactive.Active;
        }

        public void Deactivate()
        {
            Active = EActiveDeactive.Deactive;
            Audit.Delete();
        }

        public bool Authenticate(string login, string password)
        {
            if (Login == login && Password == Encrypt(password))
                return true;

            AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }
        private static string Encrypt(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));
            return sbString.ToString();
        }

        public void Validate()
        {
            AddNotifications(new Flunt.Validations.Contract().Requires()
               .HasMaxLen(Login, 15, "Login", Messages.LENGTH_15_MAX)
               .HasMinLen(Login, 5, "Login", Messages.LENGTH_05_MIN)
               .HasMaxLen(Password, 15, "Senha", Messages.LENGTH_15_MAX)
               .HasMinLen(Password, 5, "Senha", Messages.LENGTH_05_MIN)
               .IsNullOrNullable(PercentM, "Percentual M", Messages.VALUE_REQUIRED)
               .IsNullOrNullable(PercentB, "Percentual B", Messages.VALUE_REQUIRED)
               .IsGreaterOrEqualsThan(PercentM, 0, "Percentual M", Messages.VALUE_GREATER_THAN_ZERO_REQUIRED)
               .IsGreaterOrEqualsThan(PercentB, 0, "Percentual B", Messages.VALUE_GREATER_THAN_ZERO_REQUIRED)
               .AreEquals(Password, _confirmPassword, "Senha", Messages.PASSWORD_DIFFENT));

            AddNotifications(Name);
        }

        public NameOrDescription Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public EActiveDeactive Active { get; set; }
        public long Nivel { get; private set; }
        public decimal PercentM { get; private set; }
        public decimal PercentB { get; private set; }
        public virtual Audit Audit { get; protected set; }
    }
}
