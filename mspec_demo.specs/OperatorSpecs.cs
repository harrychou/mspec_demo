using Machine.Specifications;

namespace mspec_demo.specs
{
    [Subject("Operator")]
    public class OperatorSpecs
    {
        static decimal value;

        Establish context = () =>
                            value = 0m;

        Because of = () =>
                     value = new Operator().Add(42.0m, 42.0m);

        It should_add_both_operands = () =>
                                      value.ShouldEqual(84.0m);
    }


    [Subject("Operator")]
    public class when_adding_multiple_operands
    {
        static decimal value;

        Establish context = () =>
            value = 0m;

        Because of = () =>
            value = new Operator().Add(42m, 42m, 42m);

        It should_add_all_operands = () =>
            value.ShouldEqual(126m);
    }
}