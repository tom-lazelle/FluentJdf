using System.Linq;
using FluentJdf.LinqToJdf;
using Machine.Specifications;

namespace FluentJdf.Tests.Unit.TemplateEngine.Template {
    [Subject(typeof(FluentJdf.TemplateEngine.Template))]
    public class when_generating_message_from_template_with_template_level_custom_formula_taking_four_parameters {
        static FluentJdf.LinqToJdf.Message messageTemplate;
        static FluentJdf.LinqToJdf.Message messageInstance;

        Establish context = () => {
            messageTemplate = FluentJdf.LinqToJdf.Message.Create().AddCommand().SubmitQueueEntry().With()
                .Attribute("test", "[:test=customTestFunction(testParm1, testParm2, testParm3, testParm4):]").Message;
        };

        Because of =
            () => messageInstance = FluentJdf.LinqToJdf.Message.CreateFromTemplate(messageTemplate)
                                        .With()
                                        .NameValue("testParm1", "replacement1")
                                        .NameValue("testParm2", "replacement2")
                                        .NameValue("testParm3", "replacement3")
                                        .NameValue("testParm4", "replacement4")
                                        .CustomFormula("customTestFunction", 
                                            (parm1, parm2, parm3, parm4) => string.Format("this is a test {0}, {1}, {2}, {3}", parm1, parm2, parm3, parm4))
                                        .Generate();

        It should_have_result_of_function_variable_location = () => messageInstance.Descendants(Element.Command).First().GetAttributeValueOrNull("test")
            .ShouldEqual("this is a test replacement1, replacement2, replacement3, replacement4");
    }
}