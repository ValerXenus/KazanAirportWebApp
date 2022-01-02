const apiUrl = "http://localhost:32111";

export const usersMethods = {
    ADD_NEW_USER: apiUrl + "/UserAccount/AddNewUser",
    UPDATE_USER: apiUrl + "/UserAccount/UpdateUser",
    GET_USERS_LIST: apiUrl + "/UserAccount/GetUsersList",
    GET_USER_BY_ID: apiUrl + "/UserAccount/GetUserById",
    LOGIN_USER: apiUrl + "/UserAccount/LoginUser",
    REMOVE_USER: apiUrl + "/UserAccount/RemoveUser"
}

export const passengersMethods = {
    ADD_NEW_PASSENGER: apiUrl + "/Passengers/AddNewPassenger",
    UPDATE_PASSENGER: apiUrl + "/Passengers/UpdatePassenger",
    GET_PASSENGERS_LIST: apiUrl + "/Passengers/GetPassengersList",
    GET_PASSENGER_BY_ID: apiUrl + "/Passengers/GetPassengerById",
    GET_PASSENGER_BY_PASSPORT: apiUrl + "/Passengers/GetPassengerByPassport",
    REMOVE_PASSENGER: apiUrl + "/Passengers/RemovePassenger"
}

export const citiesMethods = {
    ADD_NEW_CITY: apiUrl + "/Cities/AddNewCity",
    UPDATE_CITY: apiUrl + "/Cities/UpdateCity",
    GET_CITIES_LIST: apiUrl + "/Cities/GetCitiesList",
    GET_CITY_BY_ID: apiUrl + "/Cities/GetCityById",
    REMOVE_CITY: apiUrl + "/Cities/RemoveCity"
}

export const airlinesMethods = {
    ADD_NEW_AIRLINE: apiUrl + "/Airlines/AddNewAirline",
    UPDATE_AIRLINE: apiUrl + "/Airlines/UpdateAirline",
    GET_AIRLINES_LIST: apiUrl + "/Airlines/GetAirlinesList",
    GET_AIRLINE_BY_ID: apiUrl + "/Airlines/GetAirlineById",
    REMOVE_AIRLINE: apiUrl + "/Airlines/RemoveAirline"
}

export const planesMethods = {
    ADD_NEW_PLANE: apiUrl + "/Planes/AddNewPlane",
    UPDATE_PLANE: apiUrl + "/Planes/UpdatePlane",
    GET_PLANES_LIST: apiUrl + "/Planes/GetPlanesList",
    GET_PLANE_BY_ID: apiUrl + "/Planes/GetPlaneById",
    REMOVE_PLANE: apiUrl + "/Planes/RemovePlane"
}

export const flightsMethods = {
    SAVE_FLIGHT: apiUrl + "/Flights/SaveFlight",
    GET_DEPARTURE_FLIGHTS: apiUrl + "/Flights/GetDepartureFlightsList",
    GET_ARRIVAL_FLIGHTS: apiUrl + "/Flights/GetArrivalFlightsList"
}

export const savedFlightsMethods = {
    GET_DEPARTURE_FLIGHTS: apiUrl + "/SavedFlights/GetDepartureFlightsList",
    REMOVE_FLIGHT: apiUrl + "/SavedFlights/RemoveFlight"
}

export const ticketsMethods = {
    CREATE_TICKET: apiUrl + "/Tickets/CreateTicket",
    GET_TICKET_FOR_PASSENGER: apiUrl + "/Tickets/GetTicketsByPassengerId",
    ONLINE_REGISTER: apiUrl + "/Tickets/OnlineRegisterPassenger"
}