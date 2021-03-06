using System.Linq;
using System.Xml.Linq;
using FluentJdf.LinqToJdf;
using Machine.Specifications;

namespace FluentJdf.Tests.Unit.LinqToJdf.ResourceExtensions {
    [Subject(typeof (FluentJdf.LinqToJdf.ResourceExtensions))]
    public class when_using_set_id_on_resource_with_link_in_same_jdf {
        static XElement bindingIntent;

        Establish content = () => bindingIntent = FluentJdf.LinqToJdf.Ticket.CreateIntent().Element.AddInput(Resource.BindingIntent);

        Because of = () => bindingIntent.SetId("c1");

        It should_have_one_referencing_element = () => bindingIntent.ReferencingElements().Count().ShouldEqual(1);

        It should_have_the_link_as_the_referencing_element =
            () =>
            bindingIntent.ReferencingElements().FirstOrDefault().ShouldEqual(
                bindingIntent.Document.Descendants(Resource.BindingIntent.LinkName()).FirstOrDefault());

        It should_return_a_list_of_references = () => bindingIntent.ReferencingElements().ShouldNotBeNull();
    }
}