
namespace Backend.Domain
{
	public enum DomainExceptionCode:int {
		NotFound = 404, // Follow HTTP code
	}

	public class DomainException : Exception
	{
		public int Code {get; private set; }

		public DomainException(string error, DomainExceptionCode code): base(error)
		{
			this.Code = (int)code;
		}
	}

}