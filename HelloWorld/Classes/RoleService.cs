namespace HelloWorld.Classes
{
    public class RoleService
    {
        /// <summary>
        /// determines if the role name provided grants Admin rights.
        /// </summary>
        public bool IsAdmin(string role)
        {
            if (role.Trim().ToLower() == "admin")
                return true;

            return false;
        }
    }
}
