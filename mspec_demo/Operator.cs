namespace mspec_demo
{
    public class Operator
    {
        public decimal Add(decimal firstOperand, decimal secondOperand)
        {
            return firstOperand + secondOperand;
        }

        public decimal Add(params decimal[] operands)
        {
            decimal value = 0m;

            foreach (var operand in operands)
            {
                value += operand;
            }

            return value;
        }
    }
}