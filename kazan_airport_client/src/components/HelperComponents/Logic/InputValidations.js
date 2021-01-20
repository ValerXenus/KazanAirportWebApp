export default class InputValidations {

    // Валидация длины пароля
    static validatePassword(input) {
        if (input.length === 0)
            return "- Введите пароль";

        if (input.length > 16 || input.length < 3)
            return "- Длина пароля должна быть длиной от 3 до 16 символов";

        return "";
    }

    static validateRequiredField(input, fieldName) {
        if (input.length === 0)
            return `- Не заполнено обязательное поле: ${fieldName}\n`;
        
        return "";
    }

};
