import axios, { AxiosResponse } from 'axios'
import { Message, MessageEmbed } from 'discord.js'

type dataResponse = {
  rating: number
  overallRating: number
  name: string
  place_ids: Array<string> | undefined
  _id: string
}

const getRatings = async (msg: Message): Promise<Array<dataResponse>> => {
  const user = !msg.mentions.users.size
    ? msg.author
    : msg.mentions.users.first()
  if (!user) {
    return []
  }
  try {
    const response = await axios.get<
      object,
      AxiosResponse<Array<dataResponse>>
    >(`${process.env.LUNCH_API_URL}/users/${user.id}/ratings`)
    return response.data
  } catch {
    console.error('failed')
  }
  return []
}

export default async (msg: Message): Promise<void> => {
  const ratings = await getRatings(msg)

  if (!ratings.length) {
    msg.reply('found no ratings for user')
    return
  }

  const embed = new MessageEmbed().setTitle(`Ratings:`)
  ratings.forEach((rating) =>
    embed.addField(rating.name, rating.rating.toString())
  )

  msg.channel.send({ embeds: [embed] })
}
