using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.AccessControl;
using System.IO;

namespace DE_Sensei
{
     class RsDirPermissions
    {

        string _lastError = "";
        
        /// <summary>
        /// Set Everyone Full Control permissions for selected directory
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
       public bool SetEveryoneAccess(string dirName)
        {

            try
            {
                // Make sure directory exists
                if (Directory.Exists(dirName) == false)
                    throw new Exception(string.Format("Directory {0} does not exist, so permissions cannot be set.", dirName));

                // Get directory access info
                DirectoryInfo dinfo = new DirectoryInfo(dirName);
                DirectorySecurity dSecurity = dinfo.GetAccessControl();

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

                // Set the access control
                dinfo.SetAccessControl(dSecurity);

                _lastError = String.Format("Everyone FullControl Permissions were set for directory {0}", dirName);

                return true;

            } catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }


        }

    }
}

