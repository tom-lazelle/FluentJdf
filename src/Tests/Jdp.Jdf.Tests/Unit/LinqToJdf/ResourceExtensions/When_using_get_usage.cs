﻿using System.Xml.Linq;
using Jdp.Jdf.LinqToJdf;
using Machine.Specifications;

namespace Jdp.Jdf.Tests.Unit.LinqToJdf.ResourceExtensions {
    [Subject(typeof (Jdf.LinqToJdf.ResourceExtensions))]
    public class When_using_get_usage {
        static XElement jdf;

        Establish content =
            () => {
                jdf = Ticket.Create().AddItentNode()
                    .AddInput(ElementNames.BindingIntent)
                    .AddOutput(ElementNames.FoldingIntent);

                jdf.ResourceLinkPool()
                    .AddContent(
                        new XElement(ElementNames.StitchingParams.LinkName()),
                        new XElement(ElementNames.FoldingParams.LinkName()), new XAttribute("Usage", "Nuts"));
            };

        It should_get_usage_input_when_usage_is_input =
            () => jdf.ResourceLinkPool().Element(ElementNames.BindingIntent.LinkName()).GetUsage().ShouldEqual(ResourceUsageType.Input);

        It should_get_usage_output_when_usage_is_output =
            () => jdf.ResourceLinkPool().Element(ElementNames.FoldingIntent.LinkName()).GetUsage().ShouldEqual(ResourceUsageType.Output);

        It should_get_usage_unknown_when_usage_has_invalid_value =
            () =>
            jdf.ResourceLinkPool().Element(ElementNames.FoldingParams.LinkName()).GetUsage().ShouldEqual(
                ResourceUsageType.Unknown);

        It should_get_usage_unknown_when_usage_is_null =
            () =>
            jdf.ResourceLinkPool().Element(ElementNames.StitchingParams.LinkName()).GetUsage().ShouldEqual(
                ResourceUsageType.Unknown);
    }
}