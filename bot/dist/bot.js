"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.client = void 0;
const discord_js_1 = require("discord.js");
const dotenv_1 = __importDefault(require("dotenv"));
const processor_1 = __importDefault(require("./messages/processor"));
dotenv_1.default.config();
exports.client = new discord_js_1.Client({
    intents: [
        discord_js_1.Intents.FLAGS.DIRECT_MESSAGES,
        discord_js_1.Intents.FLAGS.DIRECT_MESSAGE_REACTIONS,
        discord_js_1.Intents.FLAGS.DIRECT_MESSAGE_TYPING,
        discord_js_1.Intents.FLAGS.GUILDS,
        discord_js_1.Intents.FLAGS.GUILD_MESSAGES,
    ],
});
exports.client.on('ready', () => console.info('Reporting for Duty!!'));
exports.client.on('messageCreate', (message) => (0, processor_1.default)(message));
exports.client.login(process.env.BOT_TOKEN);
exports.default = exports.client;
