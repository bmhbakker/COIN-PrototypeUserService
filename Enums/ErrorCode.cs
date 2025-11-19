namespace PrototypeUserService.Enums;

public enum ErrorCode
{
    None,               //No error, Success
    UsernameExists,
    WeakPassword,
    PasswordMismatch,
    InvalidCredentials,
    DatabaseError,
    Timeout,
    ValueIsNull,
    Unknown
}
