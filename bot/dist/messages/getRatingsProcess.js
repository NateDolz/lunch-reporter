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
const getRatings = (msg) => __awaiter(void 0, void 0, void 0, function* () {
    const user = !msg.mentions.users.size
        ? msg.author
        : msg.mentions.users.first();
    if (!user) {
        return [];
    }
    try {
        const response = yield axios_1.default.get(`${process.env.LUNCH_API_URL}/users/${user.id}/ratings`);
        return response.data;
    }
    catch (_a) {
        console.error('failed');
    }
    return [];
});
exports.default = (msg) => __awaiter(void 0, void 0, void 0, function* () {
    const ratings = yield getRatings(msg);
    if (!ratings.length) {
        msg.reply('found no ratings for user');
        return;
    }
    const embed = new discord_js_1.MessageEmbed().setTitle(`Ratings:`);
    ratings.forEach((rating) => embed.addField(rating.name, rating.rating.toString()));
    msg.channel.send({ embeds: [embed] });
});
