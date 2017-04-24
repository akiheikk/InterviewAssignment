﻿using Appccelerate.StateMachine.Machine;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewAssignment
{

    /// <summary>
    /// Provides way to customize state reporting.
    /// 
    /// By default you cannot get container of states from Appccelerate state machine.
    /// This class can be used for that. Also it has StateToString method to enable
    /// printing hierarchical states.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    public class CustomStateMachineReporter<TState, TEvent> : IStateMachineReport<TState, TEvent>
            where TState : IComparable
    where TEvent : IComparable

    {
        IEnumerable<IState<TState, TEvent>> myStates;

        public IEnumerable<IState<TState, TEvent>> States
        {
            get
            {
                return myStates;
            }
        }

        public void Report(string name, IEnumerable<IState<TState, TEvent>> states, Initializable<TState> initialStateId)
        {
            myStates = states;
        }

        public string StateToString(TState state, string separator = ".")
        {
            foreach (var item in myStates)
            {
                if (state.CompareTo(item.Id) == 0)
                {
                    StringBuilder str = new StringBuilder(item.ToString());
                    IState<TState, TEvent> superState = item.SuperState;
                    while (null != superState)
                    {
                        str.Insert(0, superState.ToString() + separator);
                        superState = superState.SuperState;
                    }
                    return str.ToString();
                }
            }
            throw new NotImplementedException();
        }
    }
}
