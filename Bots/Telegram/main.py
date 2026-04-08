import asyncio
import logging

from aiogram import Bot, Dispatcher
from config import TOKEN
import source.settings_commands as settings
import source.added_commands as adds
import source.delete_commands as delete
import source.user_commands as comm
from database.models import init_database

from aiogram.client.session.aiohttp import AiohttpSession
from aiohttp_socks import ProxyConnector

async def main():
    #proxy_url = "socks5://localhost:1080"
    #connector = ProxyConnector.from_url(proxy_url)
    #session = AiohttpSession(connector=connector)

    bot = Bot(token=TOKEN)#, session=session)
    dispatcher = Dispatcher()

    await init_database()
    dispatcher.include_router(settings.router)
    dispatcher.include_router(adds.router)
    dispatcher.include_router(delete.router)
    dispatcher.include_router(comm.router)
    await dispatcher.start_polling(bot)

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)
    try:
        asyncio.run(main())
    except KeyboardInterrupt:
        print("Exit")