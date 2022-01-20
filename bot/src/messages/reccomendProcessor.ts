import axios, { AxiosResponse } from 'axios'
import { Message, MessageEmbed } from 'discord.js'

type dataResponse = {
  rating: number
  overallRating: number
  name: string
  place_ids: Array<string> | undefined
  _id: string
}

const getRecs = async (users: string[]): Promise<Array<dataResponse>> => {
  try {
    const response = await axios.get<
      object,
      AxiosResponse<Array<dataResponse>>
    >(`${process.env.LUNCH_API_URL}/restaurants/recommendations`, {
      params: {
        users: users.join(','),
      },
    })
    return response.data
  } catch {
    console.error('failed')
  }
  return []
}

export default async (msg: Message) => {
  const users = msg.mentions.users.map((user) => user.id)
  users.push(msg.author.id)
  if (users.length <= 1) {
    msg.reply("Need at least one user `@`'d to reccomend a selection")
    return
  }
  const recs = await getRecs(users)
  if (!recs.length) {
    msg.reply('Failed to get any reccomendations...')
  }

  const embed = new MessageEmbed().setTitle(`Reccomendations:`)
  recs.forEach((rating) =>
    embed.addField(rating.name, rating.overallRating.toString())
  )

  msg.channel.send({ embeds: [embed] })
}
