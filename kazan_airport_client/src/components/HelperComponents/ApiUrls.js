const apiUrl = "https://localhost:44377/api";

export const usersMethods = {
    ADD_NEW_USER: apiUrl + "/UserAccount/AddNewUser",
    UPDATE_USER: apiUrl + "/UserAccount/UpdateUser",
    GET_USERS_LIST: apiUrl + "/UserAccount/GetUsersList",
    GET_USER_BY_ID: apiUrl + "/UserAccount/GetUserById",
    LOGIN_USER: apiUrl + "/UserAccount/LoginUser",
    REMOVE_USER: apiUrl + "/UserAccount/RemoveUser"
}

export const passengersMethods = {
    GET_PASSENGERS_LIST: apiUrl + "/Passengers/GetPassengersList"
}