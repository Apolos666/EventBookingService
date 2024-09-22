import { getUserBasket } from "@/services/apis/baskets/getUserBasket";
import { useQuery } from "@tanstack/react-query";

export const useUserCart = () => {
  return useQuery({
    queryKey: ["userCart"],
    queryFn: () => getUserBasket(),
  })
};