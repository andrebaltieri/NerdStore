using NerdStore.OrderContext.Domain.Enums;
using NerdStore.OrderContext.Domain.Resources;
using NerdStore.OrderContext.Shared.Validation;
using NerdStore.OrderContext.Shared.ValueObjects;

namespace NerdStore.OrderContext.Domain.ValueObjects
{
    public class Document : ValueObject<Document>
    {
        public string Number { get; private set; }
        public ELocation Location { get; private set; }

        public Document(string number, ELocation location)
        {
            Number = number;
            AddNotification(Assert.IsTrue(Validate(number, location), "Document", Notifications.DocumentNumberIsInvalid));
        }

        protected override bool EqualsCore(Document other)
        {
            return Number == other.Number;
        }

        protected override int GetHashCodeCore()
        {
            return Number.GetHashCode();
        }

        public bool Validate(string document, ELocation location)
        {
            switch (location)
            {
                case ELocation.PR_BR:
                    return Validate_PT_BR(document);
                case ELocation.EN_US:
                    return Validate_EN_US(document);
                default:
                    return Validate_EN_US(document);
            }
        }

        // Melhorar esta parte
        private bool Validate_PT_BR(string document)
        {
            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string TempCPF;
            string Digito;
            int soma;
            int resto;

            document = document.Trim();
            document = document.Replace(".", "").Replace("-", "");

            if (document.Length != 11)
                return false;

            TempCPF = document.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = resto.ToString();
            TempCPF = TempCPF + Digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = Digito + resto.ToString();

            return document.EndsWith(Digito);
        }

        private bool Validate_EN_US(string document)
        {
            return true;
        }
    }
}
