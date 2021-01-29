// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

namespace Oni.TestUtilities
{
    /// <summary>
    /// Indicates an object needs initalization before being used in a test
    /// TestComponent and TestScriptableObject will automatically invoke the Initalize method
    /// when the object is instantiated
    /// </summary>
    public interface IInitializeForTest
    {
        void Initialize();
    }
}