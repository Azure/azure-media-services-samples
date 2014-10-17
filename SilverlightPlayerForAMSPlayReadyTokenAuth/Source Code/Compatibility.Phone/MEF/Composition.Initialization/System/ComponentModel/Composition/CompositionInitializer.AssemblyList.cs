// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;
using System.IO;
using System.IO.IsolatedStorage;

namespace System.ComponentModel.Composition
{
    public static partial class CompositionInitializer
    {
        // This method is the only Silverlight specific code dependency in CompositionHost
        internal static List<Assembly> GetAssemblyList()
        {
            var assemblies = new List<Assembly>();
            var applicationType = Application.Current.GetType();
            assemblies.Add(applicationType.Assembly);
            return assemblies;
        }

    }
}