"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const getRatingsProcess_1 = __importDefault(require("./getRatingsProcess"));
const rateProcessor_1 = __importDefault(require("./rateProcessor"));
const reccomendProcessor_1 = __importDefault(require("./reccomendProcessor"));
exports.default = (msg) => {
    // get command
    const commandMatch = msg.content.match(/!\w+/);
    if (!commandMatch)
        return;
    const command = commandMatch[0];
    try {
        switch (command) {
            case '!rate':
                (0, rateProcessor_1.default)(msg);
                break;
            case '!reccomend':
                (0, reccomendProcessor_1.default)(msg);
                break;
            case '!getRatings':
                (0, getRatingsProcess_1.default)(msg);
                break;
            default:
        }
    }
    catch (_a) {
        // eslint-disable-next-line
        console.error('Error processing command');
        msg.reply("Whoops... couldn't handle that for you...");
    }
};
