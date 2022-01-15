export default class UtilityMethods {

    /**
     * Конвертировать дату и время в русскую локаль
     * @param {*} dateTime 
     * @returns 
     */
    static convertDateTime = (dateTime) => {
        dateTime = new Date(dateTime);
        var dd = String(dateTime.getDate()).padStart(2, '0');
        var MM = String(dateTime.getMonth() + 1).padStart(2, '0');
        var yyyy = dateTime.getFullYear();

        var HH = String(dateTime.getHours()).padStart(2, '0');
        var minutes = String(dateTime.getMinutes()).padStart(2, '0');

        return dd + '.' + MM + '.' + yyyy + ' ' + HH + ":" + minutes;
    }

    /**
     * Получить время посадки на самолет
     * @param {*} dateTime 
     */
    static getBoardingTime = (dateTime) => {
        dateTime = new Date(dateTime);
        var boardingDateTime = new Date(dateTime - 30 * 60000);

        var HH = String(boardingDateTime.getHours()).padStart(2, '0');
        var minutes = String(boardingDateTime.getMinutes()).padStart(2, '0');

        return HH + ":" + minutes;
    }
}