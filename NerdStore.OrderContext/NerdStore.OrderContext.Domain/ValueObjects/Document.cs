using NerdStore.OrderContext.Domain.Enums;
using NerdStore.OrderContext.Domain.Resources;
using NerdStore.OrderContext.Shared.Validation;
using NerdStore.OrderContext.Shared.ValueObjects;
using System.Text.RegularExpressions;

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

        private bool Validate_PT_BR(string document)
            => ValidaDigitoVerificador(Regex.Replace(document, @"[^\d]", ""), new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 }, 11);
        
        //private static bool CNPJValido(string cnpj)
        //    => ValidaDigitoVerificador(Regex.Replace(cnpj, @"[^\d]", ""), new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 }, 14);

        private static bool ValidaDigitoVerificador(string code, int[] multipliers, int length)
        {
            if (code.Length != length || code.Distinct().Count() == 1)
                return false;

            var digits = code.Select(char.GetNumericValue).Take(length - 2).ToList();
            var rest = digits.Zip(multipliers.Skip(1), (digit, mult) => digit * mult).Sum() % 11;

            digits.Add(rest < 2 ? 0 : 11 - rest);
            rest = digits.Zip(multipliers, (digit, mult) => digit * mult).Sum() % 11;

            digits.Add(rest < 2 ? 0 : 11 - rest);
            return code == string.Join("", digits.Select(i => i.ToString()));
        }

        private bool Validate_EN_US(string document)
        {
            return true;
        }
    }
}
