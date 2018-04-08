﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

/*
 * RATING: 5 stars. Simple design pattern with Unit Tests
 * CODE REVIEW: 4.7.18
 */
namespace PJ
{
	/// <summary>
	/// Commands are reversible. Typically a command will store a Memento that allows it to reverse
	/// the state
	/// </summary>
	public abstract class Command
	{
		public enum State {
			Invalid,
			Complete,
			Reverse
		}

		protected abstract void UndoCommand();
		protected abstract void ExecuteCommand(bool redo);

		GenericStateMachine<State> state = new GenericStateMachine<State>();

		virtual public void Execute()
		{
			switch (state.state)
			{
				case State.Invalid:
					ExecuteCommand(false);
					state.SetState(State.Complete);
					break;
				case State.Reverse:
					ExecuteCommand(true);
					state.SetState(State.Complete);
					break;
				case State.Complete:
					Debug.Log("ERROR. Execute called twice for comand.");
					break;
				default:
					break;
			}
		}

		public void Undo()
		{
			if (State.Complete == state.state) {
				UndoCommand();
				state.SetState(State.Reverse);
			}
		}

		protected class TestCommand : Command {
			public int value;
			public bool didRedo;

			protected override void ExecuteCommand(bool redo)
			{
				value++;
				didRedo = redo;
			}

			protected override void UndoCommand()
			{
				value--;
			}
		}

		[Test]
		public void UnitTests() {
			var command = new TestCommand();
			command.Execute();
			Assert.AreEqual(1, command.value);
			Assert.AreEqual(false, command.didRedo);
			command.Undo();
			Assert.AreEqual(0, command.value);
			command.Execute();
			Assert.AreEqual(1, command.value);
			Assert.AreEqual(true, command.didRedo);
		}
	}

}