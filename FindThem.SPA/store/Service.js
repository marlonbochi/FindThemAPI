import Provider from "./Provider";

export default class Service {

    id = 0;
    name = "";
    timeExecution = 0;
    price = 0;
    description = "";
    materials = "";
    provider = new Provider();
    enabled = 1;
}