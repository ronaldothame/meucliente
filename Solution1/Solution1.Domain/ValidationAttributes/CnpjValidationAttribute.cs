using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Solution1.Domain.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class CnpjValidationAttribute : ValidationAttribute
{
    public CnpjValidationAttribute()
    {
        ErrorMessage = "O CNPJ fornecido é inválido.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var cnpj = Regex.Replace(value.ToString()!, @"[^\d]", "");

        if (cnpj.Length != 14)
            return new ValidationResult(ErrorMessage);

        if (new string(cnpj[0], 14) == cnpj)
            return new ValidationResult(ErrorMessage);

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;
        for (var i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        var digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;
        for (var i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        if (cnpj.EndsWith(digito))
            return ValidationResult.Success;

        return new ValidationResult(ErrorMessage);
    }
}