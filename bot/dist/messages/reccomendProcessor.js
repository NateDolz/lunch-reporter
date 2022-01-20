"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = __importDefault(require("axios"));
const discord_js_1 = require("discord.js");
const getRecs = (users) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield axios_1.default.get(`${process.env.LUNCH_API_URL}/restaurants/recommendations`, {
            params: {
                users: users.join(','),
            },
        });
        return response.data;
    }
    catch (_a) {
        console.error('failed');
    }
    return [];
});
exports.default = (msg) => __awaiter(void 0, void 0, void 0, function* () {
    const users = msg.mentions.users.map((user) => user.id);
    users.push(msg.author.id);
    if (users.length <= 1) {
        msg.reply("Need at least one user `@`'d to reccomend a selection");
        return;
    }
    const recs = yield getRecs(users);
    if (!recs.length) {
        msg.reply('Failed to get any reccomendations...');
    }
    const embed = new discord_js_1.MessageEmbed().setTitle(`Reccomendations:`);
    recs.forEach((rating) => embed.addField(rating.name, rating.overallRating.toString()));
    msg.channel.send({ embeds: [embed] });
});
