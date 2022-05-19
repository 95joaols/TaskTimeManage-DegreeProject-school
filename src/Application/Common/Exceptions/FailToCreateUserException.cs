using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions;
public class FailToCreateUserException : Exception
{
  public FailToCreateUserException() : base("Fail To Create User") { }
}
