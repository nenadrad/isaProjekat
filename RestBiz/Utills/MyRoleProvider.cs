using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RestBiz.Utills
{
    public class MyRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (RestBizContext ctx = new RestBizContext())
            {
                var menadzerSistema = (from k in ctx.MenadzeriSistema where k.Email == username select k).FirstOrDefault<MenadzerSistema>();
                if (menadzerSistema != null)
                {
                    return new[] { "MenadzerSistema" };
                }
                else
                {
                    var menadzerRestorana = (from k in ctx.MenadzeriRestorana where k.Email == username select k).FirstOrDefault<MenadzerRestorana>();
                    if (menadzerRestorana != null)
                    {
                        return new[] { "MenadzerRestorana" };
                    }
                    else
                    {
                        return new[] { "Korisnik" };
                    }

                }

            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}