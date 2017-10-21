using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMusic.Mode;

namespace NetMusic
{
    public class SongApi
    {
        #region
        private TopList _hotSongList;
        private TopList _popSongList;
        private TopList _inLanSongs;
        private TopList _hongSongs;
        private TopList _armeSongs;
        private TopList _newSongs;
        private TopList _netSongs;
        #endregion

        #region 外部
        /// <summary>
        /// 实例化此api需要执行此方法将榜单数据填充
        /// </summary>
        /// <returns></returns>
        public async Task InitAllListAsync()
        {
           _hotSongList=new TopList();
            _popSongList=new TopList();
            _inLanSongs=new TopList();
            _hongSongs=new TopList();
            _armeSongs=new TopList();
            _newSongs=new TopList();
            _netSongs=new TopList();
           await  _hotSongList.InitFromJsonAsync(26, 0, 30);
            await _popSongList.InitFromJsonAsync(4, 0, 30);
            await _inLanSongs.InitFromJsonAsync(5, 0, 30);
            await _hongSongs.InitFromJsonAsync(6, 0, 30);
            await _armeSongs.InitFromJsonAsync(3, 0, 30);
            await _newSongs.InitFromJsonAsync(27, 0, 30);
            await _netSongs.InitFromJsonAsync(28, 0, 30);
        }
        public void InitAllList()
        {
            _hotSongList = new TopList();
            _popSongList = new TopList();
            _inLanSongs = new TopList();
            _hongSongs = new TopList();
            _armeSongs = new TopList();
            _newSongs = new TopList();
            _netSongs = new TopList();
             _hotSongList.InitFromJsonAsync(26, 0, 30).Wait();
             _popSongList.InitFromJsonAsync(4, 0, 30).Wait();
             _inLanSongs.InitFromJsonAsync(5, 0, 30).Wait();
             _hongSongs.InitFromJsonAsync(6, 0, 30).Wait();
             _armeSongs.InitFromJsonAsync(3, 0, 30).Wait();
             _newSongs.InitFromJsonAsync(27, 0, 30).Wait();
             _netSongs.InitFromJsonAsync(28, 0, 30).Wait();
        }
        /// <summary>
        /// 获取封面歌曲
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Song> GetCover()
        {
            ObservableCollection<Song> tempList=new ObservableCollection<Song>();
            tempList.Add(_hotSongList.Songs[0]);
            if (tempList.All(x => x.Id != _popSongList.Songs[0].Id))
            {
                tempList.Add(_popSongList.Songs[0]);
            }
            if (tempList.All(x => x.Id != _newSongs.Songs[0].Id))
            {
                tempList.Add(_newSongs.Songs[0]);
            }
            if (tempList.All(x => x.Id != _netSongs.Songs[0].Id))
            {
                tempList.Add(_netSongs.Songs[0]);
            }
            if (tempList.All(x => x.Id != _inLanSongs.Songs[0].Id))
            {
                tempList.Add(_inLanSongs.Songs[0]);
            }
            if (tempList.All(x => x.Id != _hongSongs.Songs[0].Id))
            {
                tempList.Add(_hongSongs.Songs[0]);
            }
            if (tempList.All(x => x.Id != _armeSongs.Songs[0].Id))
            {
                tempList.Add(_armeSongs.Songs[0]);
            }
            return tempList;
        }
        #endregion
        #region  私有



        #endregion
    }
}
