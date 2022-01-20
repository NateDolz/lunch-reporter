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
const ensureUser = (author) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        yield axios_1.default.get(`${process.env.LUNCH_API_URL}/users/${author.id}`);
    }
    catch (_a) {
        yield axios_1.default.post(`${process.env.LUNCH_API_URL}/users`, {
            discord_name: author.username,
            discord_id: author.id,
        });
    }
});
const sendErrorPrompt = (msg) => msg.reply('Could not find a restaurant to rate!\n' +
    'please include a restuarant and a rating in your message like:' +
    '\'!rate "Taco Bell" 10\'');
const rateProcessor = (msg) => __awaiter(void 0, void 0, void 0, function* () {
    const user = msg.author;
    yield ensureUser(user);
    const restaurantMatch = msg.content.match(/"(\w+(\s+)?)+"/);
    if (!restaurantMatch) {
        sendErrorPrompt(msg);
        return;
    }
    const ratingMatch = msg.content.match(/\d+/);
    if (!ratingMatch) {
        sendErrorPrompt(msg);
        return;
    }
    try {
        yield axios_1.default.post(`${process.env.LUNCH_API_URL}/users/${user.id}/ratings`, {
            rating: ratingMatch[0],
            restaurant: restaurantMatch[0].replace(/"/g, ''),
        });
    }
    catch (_b) {
        console.error('Could not create rating');
    }
});
exports.default = rateProcessor;
