using System;
using System.ComponentModel.DataAnnotations;

namespace ClientesMVC.Domain.Entities.CustomValidation
{
    //Validação customizada para o campo Data de Nascimento, onde o cliente do tipo pessoa física não pode ter menos do que 19 anos
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        //Recupera o valor passado como parâmetro no data annotation customizado da propriedade 'DataNasc', na classe PessoaFisica
        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        //Sobrescreve o método de solicitação de validação padrão
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Verifica se o campo possui valor
            if(value != null)
            {
                DateTime date;
                //Verifica se o valor fornecido é válido
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    //Se a data fornecida + 19 for menor do que a data corrente, significa que o cliente tem mais do que 19 anos
                    if (date.AddYears(_minimumAge) < DateTime.Now)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Apenas clientes acima de 19 anos podem ser cadastrados");
                    }
                }
                else
                {
                    return new ValidationResult("Apenas clientes acima de 19 anos podem ser cadastrados");
                }
            }
            //Se não possui, o cliente a ser cadastrado é pessoa Jurídica
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}