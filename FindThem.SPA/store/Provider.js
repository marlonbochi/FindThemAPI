import User from "./User";

export default class Provider {

    id = 0;
    name = "";
    photo = "";
    rateAVG = 0;
    enabled = 1;
    user = new User();
    city = "";
    neighborhood = "";
    address = "";
    state = "";
    cep = "";
    number = "";
    complement = "";
}