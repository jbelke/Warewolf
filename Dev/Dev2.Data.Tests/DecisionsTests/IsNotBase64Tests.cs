/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2016 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using Dev2.Common.ExtMethods;
using Dev2.Data.Decisions.Operations;

namespace Dev2.Data.Tests.DecisionsTests
{
    /// <summary>
    /// Is Not Bse64 Decision
    /// </summary>
    public class IsNotBase64Tests
    {
        public bool Invoke(string[] cols)
        {
            if (!string.IsNullOrEmpty(cols[0]))
            {
                return !cols[0].IsBase64();
            }

            return false;
        }

        public Enum HandlesType()
        {
            return enDecisionType.IsNotBase64;
        }
    }
}