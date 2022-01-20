import { Message } from 'discord.js'
import getRatingsProcess from './getRatingsProcess'
import rateProcessor from './rateProcessor'
import reccomendProcessor from './reccomendProcessor'

export default (msg: Message) => {
  // get command
  const commandMatch = msg.content.match(/!\w+/)
  if (!commandMatch) return
  const command = commandMatch[0]

  try {
    switch (command) {
      case '!rate':
        rateProcessor(msg)
        break
      case '!reccomend':
        reccomendProcessor(msg)
        break
      case '!getRatings':
        getRatingsProcess(msg)
        break
      default:
    }
  } catch {
    // eslint-disable-next-line
    console.error('Error processing command')
    msg.reply("Whoops... couldn't handle that for you...")
  }
}
