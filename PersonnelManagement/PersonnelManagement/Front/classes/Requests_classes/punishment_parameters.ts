import Penalty from "../penalty";
import Penaltytype from "../penaltytype";

export default class Punishment_Parameters {
    constructor() {
        this.type = [];
        this.penaltytype = [];
    }
    type: Penalty[];
    penaltytype: Penaltytype[];
}