using System;
using System.Text.RegularExpressions;

public static class ValidateCnn{
    public static bool ValidateCNN(this string creditCardNumber){
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
            return expression.IsMatch(creditCardNumber);
    }
}