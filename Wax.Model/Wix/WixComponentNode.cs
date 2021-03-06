﻿namespace tomenglertde.Wax.Model.Wix
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using JetBrains.Annotations;

    using TomsToolbox.Core;
    using TomsToolbox.Desktop;

    public class WixComponentNode : WixNode
    {
        public WixComponentNode([NotNull] WixSourceFile sourceFile, [NotNull] XElement node)
            : base(sourceFile, node)
        {
        }

        [CanBeNull]
        public string Directory => GetAttribute("Directory");

        [NotNull]
        public IEnumerable<string> Files => Node
            .Descendants(WixNames.FileNode)
            // ReSharper disable once PossibleNullReferenceException
            .Where(node => node.Parent == Node)
            // ReSharper disable once AssignNullToNotNullAttribute
            .Select(node => node.GetAttribute("Id"))
            .Where(id => !string.IsNullOrEmpty(id));

        [NotNull]
        public IEnumerable<WixFileNode> EnumerateFiles([NotNull] IDictionary<string, WixFileNode> fileNodes)
        {
            return Files.Select(fileNodes.GetValueOrDefault).Where(file => file != null);
        }
    }
}
