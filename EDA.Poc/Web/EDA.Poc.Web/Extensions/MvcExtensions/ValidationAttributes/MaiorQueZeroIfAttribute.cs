using Foolproof;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EDA.Poc.Web.Extensions.MvcExtensions.ValidationAttributes
{
    public class MaiorQueZeroIfAttribute : ContingentValidationAttribute
    {
        public Operator Operator { get; private set; }
        public object DependentValue { get; private set; }
        protected OperatorMetadata Metadata { get; private set; }
        
        public MaiorQueZeroIfAttribute(string dependentProperty, Operator @operator, object dependentValue)
            : base(dependentProperty)
        {
            Operator = @operator;
            DependentValue = dependentValue;
            Metadata = OperatorMetadata.Get(Operator);

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MaiorQueZeroIfAttribute), typeof(FoolproofValidator));
            Register.Attribute(typeof(MaiorQueZeroIfAttribute));
        }

        public MaiorQueZeroIfAttribute(string dependentProperty, object dependentValue)
            : this(dependentProperty, Operator.EqualTo, dependentValue) 
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MaiorQueZeroIfAttribute), typeof(FoolproofValidator));
            Register.Attribute(typeof(MaiorQueZeroIfAttribute));
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty, DependentValue);
        }

        public override string ClientTypeName
        {
            get { return "MaiorQueZeroIf"; }
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetClientValidationParameters()
        {
            return base.GetClientValidationParameters()
                .Union(new[] {
                    new KeyValuePair<string, object>("Operator", Operator.ToString()),
                    new KeyValuePair<string, object>("DependentValue", DependentValue)
                });
        }

        public override bool IsValid(object value, object dependentValue, object container)
        {
            if (Metadata.IsValid(dependentValue, DependentValue))
                return value != null && !string.IsNullOrEmpty(value.ToString().Trim());

            return true;
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} is required due to {1} being " + Metadata.ErrorMessage + " {2}"; }
        }
    }
}