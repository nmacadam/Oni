// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

namespace Oni.Patterns
{
    /// <summary>
    /// Contracts an enumeration value to represent the state implementation
    /// </summary>
    /// <remarks>
    /// Create a child interface with methods like OnStateEnter and OnStateExit that
    /// use whatever parameters you need for the specific implementation, then hook into
    /// <see cref="OnBeforeStateTranstion"/> or <see cref="OnPostStateTranstion"/>
    /// </remarks>
    /// <typeparam name="TStateEnumeration">Enum of state IDs</typeparam>
    public interface IState<TStateEnumeration> where TStateEnumeration : System.Enum
	{
		TStateEnumeration State { get; }
	}
}