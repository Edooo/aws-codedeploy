using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bridge.Security;

namespace Playground.Models.ComponentModel
{
	/// <summary>
	/// Represents an attribute that is used to restrict access by callers to an action method.
	/// </summary>
	[AttributeUsageAttribute( AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true )]
	public class AuthorizedRolesAttribute
		: AuthorizeAttribute
	{
		static string GetRolesNamesAsList(string[] roleNames)
		{
            return string.Join(",", roleNames);
			//var buff = new StringBuilder();
			//for ( int i = 0; i < roleNames.Length; ++i ) {
			//	buff.Append( roleNames[ i ] );
			//	buff.Append( "," );
			//}

			//if ( 0 != buff.Length ) buff.Length--;

			//return buff.ToString();
		}

		static string[] ToRoleNames(string listOfRoles)
		{
			return listOfRoles.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries )
							.Distinct( StringComparer.OrdinalIgnoreCase )
							.ToArray();
		}
		/// <summary>
		/// Authorized role names.
		/// </summary>
		private string[] _roleNames;
		/// <summary>
		/// Gets or sets a ,-delimited list of authorized roles.
		/// </summary>
		public new string Roles
		{
			get { return GetRolesNamesAsList( _roleNames ); }
			set { _roleNames = ToRoleNames( listOfRoles: value ); }
		}
		/// <summary>
		/// Initializes the role authorization attribute from a collection of roles.
		/// </summary>
		/// <param name="roleName">Authorized role name.</param>
		public AuthorizedRolesAttribute(params string[] roleName)
		{
			// normalize the collection of role names.
			_roleNames = roleName.Distinct( StringComparer.OrdinalIgnoreCase ).ToArray();
		}
		/// <summary>
		/// Implements the custom authorization.
		/// </summary>
		/// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
		/// <returns><b>true</b> if the user is authorized; otherwise, <b>false</b>.</returns>
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return	BridgeSecurity.IsInAnyRole( httpContext.User, _roleNames );
		}
	}
}