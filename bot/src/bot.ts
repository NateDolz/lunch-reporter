import { Client, Intents } from 'discord.js'
import DotEnv from 'dotenv'
import processMessage from './messages/processor'

DotEnv.config()

export const client = new Client({
  intents: [
    Intents.FLAGS.DIRECT_MESSAGES,
    Intents.FLAGS.DIRECT_MESSAGE_REACTIONS,
    Intents.FLAGS.DIRECT_MESSAGE_TYPING,
    Intents.FLAGS.GUILDS,
    Intents.FLAGS.GUILD_MESSAGES,
  ],
})

client.on('ready', () => console.info('Reporting for Duty!!'))
client.on('messageCreate', (message) => processMessage(message))

client.login(process.env.BOT_TOKEN)

export default client
