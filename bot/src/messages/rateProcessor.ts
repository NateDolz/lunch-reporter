import axios from 'axios'
import { Message, User } from 'discord.js'

const ensureUser = async (author: User) => {
  try {
    await axios.get(`${process.env.LUNCH_API_URL}/users/${author.id}`)
  } catch {
    await axios.post(`${process.env.LUNCH_API_URL}/users`, {
      discord_name: author.username,
      discord_id: author.id,
    })
  }
}

const sendErrorPrompt = (msg: Message) =>
  msg.reply(
    'Could not find a restaurant to rate!\n' +
      'please include a restuarant and a rating in your message like:' +
      '\'!rate "Taco Bell" 10\''
  )

const rateProcessor = async (msg: Message) => {
  const user = msg.author
  await ensureUser(user)

  const restaurantMatch = msg.content.match(/"(\w+(\s+)?)+"/)
  if (!restaurantMatch) {
    sendErrorPrompt(msg)
    return
  }

  const ratingMatch = msg.content.match(/\d+/)
  if (!ratingMatch) {
    sendErrorPrompt(msg)
    return
  }

  try {
    await axios.post(`${process.env.LUNCH_API_URL}/users/${user.id}/ratings`, {
      rating: ratingMatch[0],
      restaurant: restaurantMatch[0].replace(/"/g, ''),
    })
  } catch {
    console.error('Could not create rating')
  }
}

export default rateProcessor
