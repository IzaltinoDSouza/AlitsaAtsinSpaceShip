namespace AASS
{
    delegate void UserDefinedActionDelegate(GameObject obj1, GameObject obj2);
    class UserDefinedAction : ICollisionAction
    {
        private UserDefinedActionDelegate _userDefined;
        public UserDefinedAction(UserDefinedActionDelegate userDefined)
        {
            _userDefined = userDefined;
        }
        public void Execute(GameObject obj1, GameObject obj2)
        {
            _userDefined(obj1,obj2);
        }
    }
}