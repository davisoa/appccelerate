//-------------------------------------------------------------------------------
// <copyright file="SingleArgumentTransitionActionHolderTest.cs" company="Appccelerate">
//   Copyright (c) 2008-2012
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.Machine.ActionHolders
{
    using System;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class SingleArgumentTransitionActionHolderTest
    {
        public interface IBase
        {
        }

        public interface IDerived : IBase
        {
        }

        [Fact]
        public void MatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);
            
            testee.Execute(A.Fake<IBase>());
        }

        [Fact]
        public void DerivedType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);

            testee.Execute(A.Fake<IDerived>());
        }

        [Fact]
        public void NonMatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);

            Action action = () => { testee.Execute(3); };

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TooManyArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);

            Action action = () => { testee.Execute(new object[] { 3, 4 }); };

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void TooFewArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);

            Action action = () => { testee.Execute(new object[] { }); };

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void EmptyDescription()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, null);

            Func<string> func = () => testee.Describe();

            func().ShouldBeEquivalentTo("BaseAction");
        }

        [Fact]
        public void PopulatedDescription()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction, "My Description");

            Func<string> func = () => testee.Describe();

            func().ShouldBeEquivalentTo("My Description");
        }

        private static void BaseAction(IBase b)
        {
        }
    }
}