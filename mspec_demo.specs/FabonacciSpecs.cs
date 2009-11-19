using Machine.Specifications;

namespace mspec_demo.specs
{
    [Subject("Fibonacci")]
    public class when_getting_fabonacci_number_greater_than_one
    {
        It position_1_should_equal_to_1 = () =>
            Fibonacci.Calculate(1).ShouldEqual(1);

        It position_2_should_equal_to_1 = () =>
            Fibonacci.Calculate(2).ShouldEqual(1);

        It position_3_should_equal_to_2 = () =>
            Fibonacci.Calculate(3).ShouldEqual(2);

        It position_4_should_equal_to_3 = () =>
            Fibonacci.Calculate(4).ShouldEqual(3);

        It position_6_should_equal_to_8 = () =>
            Fibonacci.Calculate(6).ShouldEqual(8);

    }

}