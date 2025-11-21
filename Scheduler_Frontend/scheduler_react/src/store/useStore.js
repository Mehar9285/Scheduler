import {create} from "zustand";


export const useStore = create((set) => ({
  myList: [],            
  nowPlaying: null,      
  
  addToList: (show) =>
    set((state) => ({ myList: [...state.myList.filter(s => s.id !== show.id), show] })),
  
  removeFromList: (id) =>
    set((state) => ({ myList: state.myList.filter((s) => s.id !== id) })),
  
  setNowPlaying: (show) => set({ nowPlaying: show }),
}));