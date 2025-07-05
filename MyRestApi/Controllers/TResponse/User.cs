using Npgsql.Replication;

public class UserResponse : BaseResponse
{
    // public UserResponse() : base(statusCode: 200, null) 
    public UserResponse()
    {
        this.Data = null;
        this.StatusCode = 200;

    }
}