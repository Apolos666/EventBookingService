import { postUserBasket } from "@/services/apis/baskets/postUserBasket";
import { EventCartDto } from "@/services/apis/baskets/types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export function useAddUserCart() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (event: EventCartDto) => postUserBasket(event),
        onSettled: async (_) => {
            await queryClient.invalidateQueries({ queryKey: ["userCart"] });
        }
    })
}