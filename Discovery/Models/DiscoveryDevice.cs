﻿using Onvif.Core.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Onvif.Core.Discovery.Models
{
    public class DiscoveryDevice : IEquatable<DiscoveryDevice>
    {
        public IEnumerable<string> Types { get; internal set; }
        public IEnumerable<string> XAdresses { get; internal set; }
        public string Model { get; internal set; }
        public string Name { get; internal set; }
        public IPAddress Address { get; internal set; }

        public override bool Equals(object obj)
        {
            return obj is DiscoveryDevice o &&
                    Equals(o);
        }

        public bool Equals(DiscoveryDevice o)
        {
            if (o == null)
            {
                return false;
            }

            if (Address == null)
            {
                if (o.Address != null)
                {
                    return false;
                }
            }
            else if (!Address.Equals(o.Address))
            {
                return false;
            }

            if (o.Model != Model || o.Name != Name
                || o.Types.Count() != Types.Count() || o.XAdresses.Count() != XAdresses.Count())
            {
                return false;
            }
            for (var i = 0; i < Types.Count(); i++)
            {
                if (!Types.ElementAt(i).Equals(o.Types.ElementAt(i)))
                {
                    return false;
                }
            }
            for (var i = 0; i < XAdresses.Count(); i++)
            {
                if (!XAdresses.ElementAt(i).Equals(o.XAdresses.ElementAt(i)))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            var fnv1 = new Fnv1aImpl();
            fnv1.AppendObjs(Types);
            fnv1.AppendObjs(XAdresses);
            fnv1.AppendObj(Model);
            fnv1.AppendObj(Name);
            fnv1.AppendObj(Address);
            return fnv1.Hash;
        }
    }
}
