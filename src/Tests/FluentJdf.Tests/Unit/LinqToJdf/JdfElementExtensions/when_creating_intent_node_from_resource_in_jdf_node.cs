using System.Linq;
using System.Xml.Linq;
using FluentJdf.LinqToJdf;
using Machine.Specifications;

namespace FluentJdf.Tests.Unit.LinqToJdf.JdfElementExtensions {
    [Subject("Fluent Creation of Intent Nodes")]
    public class when_creating_intent_node_from_resource_in_jdf_node {
        static XElement resource;
        static XElement newIntent;

        Establish content = () => {
            var doc = new XDocument();
            doc.Add(new XElement(Element.JDF));
            resource = doc.Root.AddInput(Resource.BindingIntent);
        };

        Because of = () => newIntent = resource.AddIntentElement();

        It should_create_jdf_node_off_nearest_jdf_parent_of_resource = () => newIntent.Document.Root.Descendants(Element.JDF).Count().ShouldEqual(1);

        It should_have_created_jdf_intent_node_off_nearest_jdf_parent_of_resource =
            () => newIntent.Document.Root.Descendants(Element.JDF).FirstOrDefault().IsJdfIntentElement().ShouldBeTrue();

        It should_have_job_part_id_on_the_newly_created_intent_node =
            () => newIntent.Document.Root.Descendants(Element.JDF).FirstOrDefault().GetJobPartId().ShouldNotBeNull();

        It should_not_have_child_of_resource = () => resource.Elements().Count().ShouldEqual(0);
    }
}