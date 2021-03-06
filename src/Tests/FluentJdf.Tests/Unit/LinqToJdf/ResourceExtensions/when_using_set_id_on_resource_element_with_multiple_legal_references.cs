using System.Linq;
using System.Xml.Linq;
using FluentJdf.LinqToJdf;
using Machine.Specifications;

namespace FluentJdf.Tests.Unit.LinqToJdf.ResourceExtensions {
    [Subject(typeof (FluentJdf.LinqToJdf.ResourceExtensions))]
    public class when_using_set_id_on_resource_element_with_multiple_legal_references {
        static XElement bindingIntent;

        Establish content = () => {
                                bindingIntent = FluentJdf.LinqToJdf.Ticket.CreateIntent().Element.AddInput(Resource.BindingIntent);
                                bindingIntent.NearestJdf().AddIntentElement().ResourceLinkPoolElement().Add(new XElement("Tom",
                                                                                                              new XAttribute("rRef",
                                                                                                                             bindingIntent.GetId())));
                            };

        Because of = () => bindingIntent.SetId("b7");

        It should_have_new_id_as_set = () => bindingIntent.GetId().ShouldEqual("b7");

        It should_have_two_references = () => bindingIntent.ReferencingElements().Count().ShouldEqual(2);

        It should_have_the_additional_referencing_node_as_one_of_the_referencing_element =
            () => bindingIntent.ReferencingElements().Where(r => r.Name == "Tom").FirstOrDefault().ShouldNotBeNull();

        It should_have_the_link_as_one_of_the_referencing_element =
            () => bindingIntent.ReferencingElements().Where(r => r.Name == Resource.BindingIntent.LinkName()).FirstOrDefault().ShouldNotBeNull();
    }
}