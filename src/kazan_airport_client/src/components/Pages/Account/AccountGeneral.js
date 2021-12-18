import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';
import { ManageUsers } from './ManagementComps/ManageUsers';
import { ManagePassengers } from './ManagementComps/ManagePassengers';
import { ManageCities } from './ManagementComps/ManageCities';
import { ManagePlanes } from './ManagementComps/ManagePlanes';
import { ManageAirlines } from './ManagementComps/ManageAirlines';
import { ManageFlights } from './ManagementComps/ManageFlights';
import WelcomeAccount from './WelcomeAccount';
import Cookies from 'js-cookie';
import { PassengerTickets } from './PassengerComps/PassengerTickets';

const AccountGeneral = () => {

    // Если пользователь не авторизован
    var authCookie = Cookies.get("authData");
    if (!authCookie) {
        window.location = "/";
        return;
    }

    // Проверка, что в Cookie есть запись об авторизованном пользователе
    let currentSession = JSON.parse(authCookie);
    if (!currentSession || 
        (currentSession !== undefined && !currentSession.isAuth)) {
        window.location = "/";
        return;
    }

    let collection = {};

    switch (currentSession.role) {
        case 0:
            collection = { data: [
                { link: "/passenger/tickets", name: "Мои билеты", component: <PassengerTickets />, key: "p_tickets" }],
                main: { link: "/passenger", component: <WelcomeAccount /> },
                needShowLogout: true
            };
            break;
        case 1:
            collection = { data: [
                { link: "/operator/passengers", name: "Пассажиры", component: <ManagePassengers />, key: "o_passengers" },
                { link: "/operator/cities", name: "Города", component: <ManageCities />, key: "o_cities" },
                { link: "/operator/planes", name: "Самолеты", component: <ManagePlanes />, key: "o_planes" },
                { link: "/operator/airlines", name: "Авиакомпании", component: <ManageAirlines />, key: "o_airlines" },
                { link: "/operator/flights", name: "Рейсы", component: <ManageFlights />, key: "o_flights" }],
                main: { link: "/operator", component: <WelcomeAccount /> },
                needShowLogout: true
            };
            break;
        case 2:
            collection = { data: [
                { link: "/admin/users", name: "Пользователи", component: <ManageUsers />, key: "a_users" },
                { link: "/admin/passengers", name: "Пассажиры", component: <ManagePassengers />, key: "a_passengers" },
                { link: "/admin/cities", name: "Города", component: <ManageCities />, key: "a_cities" },
                { link: "/admin/planes", name: "Самолеты", component: <ManagePlanes />, key: "a_planes" },
                { link: "/admin/airlines", name: "Авиакомпании", component: <ManageAirlines />, key: "a_airlines" },
                { link: "/admin/flights", name: "Рейсы", component: <ManageFlights />, key: "a_flights" }],
                main: { link: "/admin", component: <WelcomeAccount /> },
                needShowLogout: true 
            };
            break;
        default:
            window.location = "/";
            return;
    }

    return (
        <div>
            
            <SubchapterPage collection={collection} />
        </div>
    );
}

export default AccountGeneral;
