"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.server = exports.client = void 0;
const discord_js_1 = require("discord.js");
const dotenv_1 = __importDefault(require("dotenv"));
const http_1 = __importDefault(require("http"));
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
exports.server = http_1.default.createServer((req, res) => {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('Need open port for bot\n');
});
exports.server.listen(8080, () => {
    console.log('listening on port', 8080);
});
exports.client.login(process.env.BOT_TOKEN);
exports.default = exports.client;
