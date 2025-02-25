namespace JEPCO.Shared.Constants;

public class LocalizationKeysConstant
{
    #region success messages
    public const string DataRetrieved = "DataRetrieved";
    public const string ResourceCreated = "ResourceCreated";
    public const string ResourceUpdated = "ResourceUpdated";
    public const string ResourceDeleted = "ResourceDeleted";
    public const string LinkHasBeenSent = "LinkHasBeenSent";
    public const string ChargingStarted = "ChargingStarted";
    public const string ChargingStopped = "ChargingStopped";
    public const string ChargingCanceled = "ChargingCanceled";
    public const string NoData = "NoData";
    public const string ResourceAddedToFavorite = "ResourceAddedToFavorite";
    public const string ResourceRemovedFromFavorite = "ResourceRemovedFromFavorite";

    #endregion

    #region error messages
    // defaults
    public const string UnAuthorizedDefaultMessage = "UnAuthorizedDefaultMessage";
    public const string BadRequestDefaultMessage = "BadRequestDefaultMessage";
    public const string NotFoundDefaultMessage = "NotFoundDefaultMessage";
    public const string InternalServerErrorDefaultMessage = "InternalServerErrorDefaultMessage";
    public const string ConflictDefaultMessage = "ConflictDefaultMessage";
    public const string ItemDoesNotExist = "ItemDoesNotExist";
    public const string ItemAlreadyActive = "ItemAlreadyActive";
    public const string ItemAlreadyNotActive = "ItemAlreadyNotActive";
    public const string ActivateSiteWithoutReseller = "ActivateSiteWithoutReseller";
    public const string ActivateChargingPointWithoutSite = "ActivateChargingPointWithoutSite";
    public const string ActivateConnectorWithoutChargingPoint = "ActivateConnectorWithoutChargingPoint";


    public const string PhoneNumberNotCompleted = "PhoneNumberNotCompleted";
    public const string LanguageNotSupported = "LanguageNotSupported";

    public const string UserAlreadyExists = "UserAlreadyExists";

    //images
    public const string FileSizeErrorMessage = "FileSizeErrorMessage";
    public const string AllowedFileExtentionsErrorMessage = "AllowedFileExtentionsErrorMessage";

    // consumer
    public const string ConsumerProfileDeletionErrorMessage = "ConsumerProfileDeletionErrorMessage";

    #endregion
    #region other
    #endregion
}
